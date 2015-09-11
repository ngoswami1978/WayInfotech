<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TASR_DeinstallMisc.aspx.vb"
    Inherits="TravelAgency_TASR_DeinstallMisc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Miscellineous DeInstallation </title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>

<script type="text/javascript" src="../JavaScript/AAMS.js"></script>

<body>
    <form id="form1" runat="server">
        <table width="860px" align="left" style="height: 100px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left">
                                            <span class="menu">Travel Agency-&gt;</span><span class="sub_menu">Misc. Deinstalltion
                                                </span>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#1A61A9" class="heading" align="center" style="width: 1000px;">
                                            Agency Misc. DeInstallation </td>
                                        <td bgcolor="#1A61A9">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                          <tr>
                            <td align="right" style ="width:860px;">
                                <a href="#" class="LinkButtons" onclick="window.close();">Close</a>&nbsp;&nbsp;&nbsp;</td>
                        </tr> 
                        <tr>
                            <td valign="top" style="width: 860px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td valign="top" class="redborder" style="width: 860px">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" align="center" valign="TOP" rowspan="0" style="height: 23px;"
                                                        colspan="2">
                                                        <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                   <td>
                                                       <table width="860px" border="0" cellpadding="2" cellspacing="0">
                                                                <tr>
                                                               <td align="right" class="textbold">
                                                                     <asp:Button ID="btnExport" runat="server" CssClass="button" Text="Export" />
                                                                   </td>
                                                                </tr>
                                                            
                                                            <tr>
                                                                <td  colspan ="6" align="right"  class="textbold" >
                                                                     <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" /></td>
                                                            </tr>   
                                                            <tr>
                                                              <td colspan ="6" ></td>
                                                            </tr>                                                        
                                                       </table>
                                                   </td>
                                                </tr>
                                                <tr>
                                                    <td width="804px" valign="top">
                                                        <asp:GridView ID="gvMiscDeInstallation" AllowSorting="true" runat="server" HeaderStyle-ForeColor="White"
                                                            EnableViewState="false" AutoGenerateColumns="False" TabIndex="5" Width="804px">
                                                            <Columns>
                                                                                    <asp:TemplateField HeaderText="Date Inst." SortExpression="INSTALLATIONDATE"  ItemStyle-Wrap="false">
                                                                                        <ItemTemplate>
                                                                                            <%#Eval("INSTALLATIONDATE")%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    
                                                                                    <asp:TemplateField HeaderText="Date DeInst." SortExpression="DEINSTALLATIONDATE"  ItemStyle-Wrap="false">
                                                                                        <ItemTemplate>
                                                                                            <%#Eval("DEINSTALLATIONDATE")%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField SortExpression="EQUIPMENT_TYPE" DataField="EQUIPMENT_TYPE" HeaderText="Equipment Type">
                                                                                        <ItemStyle Wrap="False" />
                                                                                        <HeaderStyle Wrap="False" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField SortExpression="EQUIPMENT_NO" DataField="EQUIPMENT_NO" HeaderText="Equipment No.">
                                                                                        <ItemStyle Wrap="False" />
                                                                                        <HeaderStyle Wrap="False" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField SortExpression="QTY" DataField="QTY" HeaderText="Equipment Qty." />
                                                                                   <asp:BoundField SortExpression="ChallanNumber" DataField="ChallanNumber" HeaderText="Challan No.">
                                                                                        <ItemStyle Wrap="False" />
                                                                                        <HeaderStyle Wrap="False" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField SortExpression="LOGGEDBY" DataField="LOGGEDBY" HeaderText="Logged By">
                                                                                        <ItemStyle Wrap="False" />
                                                                                        <HeaderStyle Wrap="False" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField SortExpression="LOGGEDDATETIME" DataField="LOGGEDDATETIME" HeaderText="Logged Date Time">
                                                                                        <ItemStyle Wrap="False" />
                                                                                        <HeaderStyle Wrap="False" />
                                                                                    </asp:BoundField>
                                                                                   </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <RowStyle CssClass="textbold" />
                                                            <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                        </asp:GridView>
                                                    </td>
                                                    <td valign="top" rowspan="2">
                                                       <%-- <table cellpadding="0" cellspacing="0" border="0">
                                                            <tr>
                                                                <td>
                                                                  
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                   
                                                                </td>
                                                            </tr>
                                                        </table>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
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
                                                </tr>
                                            </table>
                                            <asp:TextBox ID="txtRecordOnCurrReco" Visible="false" runat="server"></asp:TextBox>
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
