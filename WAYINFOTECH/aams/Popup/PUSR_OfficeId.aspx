<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PUSR_OfficeId.aspx.vb" Inherits="Popup_PUSR_OfficeId" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title>AAMS:Manage Office ID</title>
    <base target="_self" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    

    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<script language="javascript" type="text/javascript">
    function ResetFields()
    {
        document.getElementById("txtOfficeId").value="";
        document.getElementById("drpCity").value="";
        document.getElementById("txtOfficeId").focus();  
       return false;
    }
    
    </script>
<body onunload="return false;">
    <form id="form1" defaultbutton="btnSearch" defaultfocus="txtOfficeId" runat="server">
        <table width="860px" align="left" class="border_rightred" style="height: 486px;">
            <tr>
                <td valign="top" style="height: 482px">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Travel Agency-></span><span class="sub_menu">Office ID</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Office Id</td>
                        </tr>
                        <tr>
                            <td valign="top" style="height: 314px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" style="height: 15px">
                                                        &nbsp;<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 299px; height: 19px;">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="width: 380px; height: 19px;">
                                                    </td>
                                                    <td style="width: 354px; height: 19px;">
                                                    </td>
                                                    <td style="width: 176px; height: 19px;">
                                                    </td>
                                                    <td width="15%" style="height: 19px">
                                                    </td>
                                                    <td width="15%" style="height: 19px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 299px; height: 22px;">
                                                    </td>
                                                    <td class="textbold" style="width: 380px; height: 22px;" align="left">
                                                        Office ID</td>
                                                    <td style="width: 354px; height: 22px;">
                                                        <asp:TextBox ID="txtOfficeId" runat="server" CssClass="textbox" MaxLength="30" TabIndex="1"
                                                            Width="160px"></asp:TextBox></td>
                                                    <td class="textbold" style="width: 176px; height: 22px;" align="center">
                                                        </td>
                                                    <td style="width: 20%; height: 22px;">
                                                        &nbsp;<%-- <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey"  ReadOnly="true" MaxLength="30" TabIndex="2"
                                                            Width="160px"></asp:TextBox>--%></td>
                                                    <td style="width: 23%; height: 22px;">
                                                        <asp:Button ID="btnSearch" runat="server" TabIndex="3" CssClass="button" Text="Search"
                                                            OnClick="btnSearch_Click" /></td>
                                                </tr>
                                                <tr>
                                                    <td align="center" class="textbold" colspan="6" valign="top" >
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 299px; height: 25px">
                                                    </td>
                                                    <td class="textbold" style="width: 380px; height: 25px;">
                                                        City</td>
                                                    <td class="textbold" style="width: 354px; height: 25px;">
                                                        <asp:DropDownList ID="drpCity" Enabled="false" runat="server" TabIndex="2" Width="168px"
                                                            CssClass="textbold">
                                                        </asp:DropDownList></td>
                                                    <td class="textbold" style="width: 176px; height: 25px;">
                                                    </td>
                                                    <td style="height: 25px">
                                                    </td>
                                                    <td style="height: 25px">
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" TabIndex="5" Text="Reset" /></td>
                                                </tr>
                                                 <tr>
                                                    <td align="center" class="textbold" colspan="6" valign="top" >
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px; width: 299px;">
                                                    </td>
                                                    <td class="textbold" style="width: 380px; height: 25px;">
                                                        Corporate Code</td>
                                                    <td class="textbold" style="width: 354px; height: 25px;">
                                                        <asp:TextBox ID="ddlCorporateCode" runat="server" CssClass="textboxgrey" MaxLength="1" TabIndex="4" Width="160px"></asp:TextBox>
                                                        </td>
                                                    <td class="textbold" style="width: 176px; height: 25px;">
                                                    </td>
                                                    <td style="height: 25px">
                                                        </td>
                                                    <td style="height: 25px">
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 299px; height: 25px">
                                                    </td>
                                                    <td style="width: 380px">
                                                    </td>
                                                    <td class="textbold" style="width: 354px">
                                                        <asp:CheckBox ID="chkunallocatedid" class="textbold" runat="server" Text="Unallocated OfficeId"
                                                            TabIndex="4" Width="168px" Checked="True" /></td>
                                                    <td class="textbold" style="width: 176px">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="7" style="height: 4px">
                                                        <asp:GridView ID="grdAgencyOfficeId" BorderWidth="1" BorderColor="#d4d0c8" runat="server"
                                                            AutoGenerateColumns="False" ItemStyle-CssClass="ItemColor" AlternatingItemStyle-CssClass="lightblue"
                                                            HeaderStyle-CssClass="Gridheading" Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Office Id">
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="HiddenOfficeId" runat="server" Value='<%#Eval("OFFICEID")%>' />
                                                                        <%#Eval("OFFICEID")%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left"  Width="75px"/>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Agency">
                                                                    <ItemTemplate>
                                                                        <%#Eval("NAME")%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left"  Width="300px"/>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CID">
                                                                    <ItemTemplate>
                                                                        <%#Eval("CID")%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" Width="75px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Date Of Processing">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPD" runat="server" Text='<%#Eval("PROCESSING_DATE")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Terminal Id">
                                                                    <ItemTemplate>
                                                                        <%#Eval("TERMINALID")%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" Width="75px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Remarks">
                                                                    <ItemTemplate>
                                                                        <%#Eval("REMARKS")%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action" >
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="LinkButton1" CssClass="LinkButtons" Text="Select" runat="server"
                                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OFFICEID")  %>'></asp:LinkButton>
                                                                   </ItemTemplate>
                                                                   <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <RowStyle CssClass="textbold" />
                                                            <HeaderStyle CssClass="Gridheading" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Literal ID="litOfficeId" runat="server"></asp:Literal></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
