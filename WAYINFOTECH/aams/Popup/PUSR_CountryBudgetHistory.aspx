<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PUSR_CountryBudgetHistory.aspx.vb" Inherits="Popup_PUSR_CountryBudgetHistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>HelpDesk:PTR Assignee History</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <base target="_self"/>
     <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<body  >
    <form id="frmPtrHistory" runat="server" >
        <table width="860px" align="left" height="486px" >
            <tr>
                <td valign="top">
                    <table width="100%" align="left">  
                         <tr>
                            <td align="right">
                                <a href="#" class="LinkButtons" onclick="window.close();">Close</a>&nbsp;&nbsp;&nbsp;</td>
                        </tr>                      
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Country Wise Budget History</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                 
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="4" align="center">
                                                                    <asp:GridView ID="grdvPtrAssigneeHistory" AllowSorting="true" HeaderStyle-ForeColor="white"  runat="server" BorderWidth="1" BorderColor="#d4d0c8" AutoGenerateColumns="False"
                                                                        Width="95%" TabIndex="9">
                                                                        <Columns>
                                                                       <asp:BoundField DataField="COUNTRYNAME" SortExpression="COUNTRYNAME" HeaderText="Country Name" HeaderStyle-HorizontalAlign="left" />
                                                                        <asp:TemplateField HeaderText="Month" SortExpression="MONTH">
                                                                        <ItemTemplate>
                                                                        <%# MonthName(Eval("MONTH"),False) %>
                                                                        </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        
                                                                        <asp:BoundField DataField="YEAR" SortExpression="YEAR" HeaderText="Year" HeaderStyle-HorizontalAlign="left" />
                                                                        
                                                                        <asp:BoundField DataField="USERNAME" SortExpression="USERNAME" HeaderText="Logged By" HeaderStyle-HorizontalAlign="left" />
                                                                        
                                                                        <asp:BoundField DataField="DATE" SortExpression="DATE" HeaderText="Date & Time" HeaderStyle-HorizontalAlign="left" />
                                                                        
                                                                        <asp:BoundField DataField="CHANGED" SortExpression="CHANGED" HeaderText="Changed Data" HeaderStyle-HorizontalAlign="left" />
                                                                        
                                                                    </Columns>                                                                                                                                    
                                                                    <HeaderStyle CssClass="Gridheading"/>
                                                                    <RowStyle CssClass="ItemColor" HorizontalAlign="Left"/>
                                                                    <AlternatingRowStyle  CssClass ="lightblue" HorizontalAlign="Left" />
                                                                    </asp:GridView>
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="12">
                                                                </td>
                                                            </tr>
                                                             <!-- code for paging----->
                                            <tr>                                                   
                                                    <td valign ="top" colspan ="6"  >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                      <tr class="paddingtop paddingbottom">                                                                                                                                                
                                                                          <td style="width: 243px" class="left" nowrap="nowrap"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  ReadOnly="true" Width="105px" CssClass="textboxgrey" TabIndex="5" ></asp:TextBox></td>
                                                                          <td style="width: 200px" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev" TabIndex="5"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 356px" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" TabIndex="5" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 187px" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next" TabIndex="5">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                    </td>
                                                </tr>
                                                
            <!-- code for paging----->
                                                        </table>
                                                  
                                        </td>
                                    </tr>
                                </table>
                                
                                <asp:Literal ID="litAgencyGroup" runat="server" ></asp:Literal></td>
                        </tr>
                    </table>
                    
                                
               
            </tr>
        </table>
    </form>
</body>
</html>
