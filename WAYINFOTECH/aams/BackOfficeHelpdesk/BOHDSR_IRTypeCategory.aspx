<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BOHDSR_IRTypeCategory.aspx.vb"
    Inherits="BOHelpDesk_HDSR_IRTypeCategory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Search Back Office Helpdesk IR Type Catgory</title>

    <script src="../JavaScript/BOHelpDesk.js" type="text/javascript"></script>

    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript">
    
    function EditFunction(CheckBoxObj)
   {           
          window.location.href="BOHDUP_IRTypeCategory.aspx?Action=U|"+CheckBoxObj;               
          return false;
    }
       
        
    function DeleteFunction(ISPId)
    {   
        if (confirm("Are you sure you want to delete?")==true)
        {   
        
        document.getElementById("hdDelete").value=ISPId
        return true;

        }
        else
        {
        document.getElementById("hdDelete").value="";
        return false;
        }
    }
    </script>

</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSearch">
        <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Back Office Help Desk-></span><span class="sub_menu">IR Type Category</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" style="height: 10px">
                                IR Type Category</td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td width="100%" class="redborder">
                                            <table align="left" border="0" cellpadding="2" cellspacing="0" width="100%">
                                                <tr>
                                                    <td class="textbold" style="height: 28px; width: 100%" colspan="4" valign="top">
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td align="center" class="textbold" colspan="5" height="20px" valign="top">
                                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px; width: 10%;">
                                                                </td>
                                                                <td class="textbold" style="width: 10%; height: 25px;">
                                                                </td>
                                                                <td align="left" style="width: 20%; height: 25px;" nowrap="nowrap">
                                                                    IR Type Category</td>
                                                                <td class="textbold" style="width: 20%; height: 25px;">
                                                                    <asp:TextBox ID="txtIRTypeCat" runat="server" CssClass="textbold" TabIndex="1" Width="224px"
                                                                        MaxLength="25"></asp:TextBox>
                                                                </td>
                                                                <td width="15%" align="right">
                                                                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" TabIndex="3"
                                                                        AccessKey="a" /></td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr height="10px">
                                                                <td colspan="6">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px; width: 10%;">
                                                                </td>
                                                                <td class="textbold" style="width: 10%;">
                                                                </td>
                                                                <td align="left" style="height: 25px; width: 162px;">
                                                                </td>
                                                                <td class="textbold" style="height: 25px">
                                                                </td>
                                                                <td style="height: 25px" align="right">
                                                                    <asp:Button ID="btnNew" runat="server" CssClass="button" Text="New" TabIndex="4"
                                                                        AccessKey="n" /></td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr height="10px">
                                                                <td colspan="6">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 10%">
                                                                </td>
                                                                <td align="left" style="width: 162px">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" TabIndex="5"
                                                                        AccessKey="r" /></td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr height="10px">
                                                                <td colspan="6">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                </td>
                                                                <td class="textbold" style="width: 10%">
                                                                </td>
                                                                <td align="left" style="width: 162px">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Button ID="btnExport" runat="server" CssClass="button" Text="Export" TabIndex="5"
                                                                        AccessKey="e" /></td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr height="10px">
                                                                <td colspan="6">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                </td>
                                                                <td colspan="2" class="ErrorMsg" style="height: 15px">
                                                                </td>
                                                                <td style="height: 15px">
                                                                    &nbsp;</td>
                                                                <td style="height: 15px">
                                                                    &nbsp;</td>
                                                                <td style="height: 15px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 15px; width: 128px;">
                                                                    &nbsp;</td>
                                                                <td colspan="2" style="height: 15px">
                                                                </td>
                                                                <td style="width: 176px; height: 15px;">
                                                                    &nbsp;</td>
                                                                <td style="height: 15px">
                                                                    &nbsp;</td>
                                                                <td style="height: 15px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="4">
                                                                </td>
                                                            </tr>
                                                            
                                                           
                                                            
                                                            
                                                            <tr>
                                                                <td style="width: 128px">
                                                                    
                                                                </td>
                                                                <td colspan="4" align="center">
                                                                    <asp:GridView ID="grdvIRTypeCat" AllowSorting="true" HeaderStyle-ForeColor="white"
                                                                        BorderWidth="1" BorderColor="#d4d0c8" runat="server" AutoGenerateColumns="False"
                                                                        Width="69%" EnableViewState="false">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="IR Type Category" SortExpression="HD_IR_TYCAT_NAME">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("HD_IR_TYCAT_NAME")%>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Action">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                <ItemTemplate>
                                                                                    <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a> &nbsp;
                                                                                    <asp:LinkButton ID="linkDelete" runat="server" CssClass="LinkButtons" Text="Delete"></asp:LinkButton>
                                                                                    <asp:HiddenField ID="hdField" runat="server" Value='<%#Eval("HD_IR_TYCAT_ID") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                        <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" ForeColor="White" />
                                                                        <RowStyle CssClass="textbold" />
                                                                    </asp:GridView>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td colspan="6" height="4">&nbsp;
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td>
                                                                </td>
                                                                <td class="textbold" colspan="5">
                                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="80%">
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                            <tr class="paddingtop paddingbottom">
                                                                                <td style="width: 35%" class="left">
                                                                                    <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                                        ID="txtTotalRecordCount" runat="server" Width="70px" CssClass="textboxgrey" ReadOnly="True"></asp:TextBox></td>
                                                                                <td style="width: 20%" class="right">
                                                                                    <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                                <td style="width: 25%" class="center">
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
                                                        <br />
                                                    </td>
                                                    <td width="18%" rowspan="1" valign="top">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="5">
                                                    </td>
                                                </tr>
                                                <tr style="font-size: 12pt; font-family: Times New Roman">
                                                    <td colspan="6" height="12">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:TextBox ID="txtRecordOnCurrReco" Visible="false" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="hdDelete" runat="server" />
                </td>
            </tr>
            <tr>
                <td valign="top" style="height: 23px">
                    &nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
