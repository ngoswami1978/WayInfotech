<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PUSR_AgencyTarget.aspx.vb" Inherits="Popup_PUSR_AgencyTarget" %>

 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Agency Target History</title>
    <base target="_self" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    </head>
      
    
<body>
    <form id="form1" runat="server">
        <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="right">
                    <%--<a href="#" class="LinkButtons" onclick="PrintHistory();">Print</a>&nbsp;&nbsp;--%>
                    &nbsp; <a href="#" class="LinkButtons" onclick="window.close();">Close</a>&nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td align="center" class="heading">
                    Agency Target History</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <%--<asp:DataGrid ID="grdHistory" runat="server" Width="100%" EnableViewState="false"
                        HeaderStyle-CssClass="Gridheading" AlternatingItemStyle-CssClass="lightblue"
                        ItemStyle-CssClass="ItemColor" AutoGenerateColumns="False" BorderColor="#d4d0c8"
                        BorderWidth="1">--%>
                        <asp:GridView ID="grdHistory" BorderWidth="1" BorderColor="#d4d0c8" runat="server"
                                                            AutoGenerateColumns="False" Width="100%" EnableViewState="true" AllowSorting="True"
                                                            HeaderStyle-ForeColor="white">
                        <Columns>
                            <asp:TemplateField HeaderText="Year" SortExpression="Year">
                                <ItemTemplate>
                                    <%#Eval("Year")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                           <%-- <asp:TemplateColumn  HeaderStyle-HorizontalAlign="Left" HeaderText="Month" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <%#Eval("Month")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateColumn>--%>
                            
                            <asp:TemplateField HeaderText="EmployeeName" SortExpression="EmpName">
                                <ItemTemplate>
                                    <%#Eval("EmpName")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                           </asp:TemplateField>
                            <asp:TemplateField  HeaderStyle-HorizontalAlign="Left" HeaderText="DateTime" SortExpression="Date" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <%#Eval("Date")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="History" SortExpression="ChangedData">
                                <ItemTemplate>
                                    <%#Eval("ChangedData")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                             </asp:TemplateField>
                             
                            
                        </Columns>
                         <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="left" />
                                                            <RowStyle CssClass="textbold" />
                                                        </asp:GridView>
                </td>
            </tr>
             <tr>                                                   
                                                    <td   valign ="top"  >
                                         <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr style="padding-top : 4pt; padding-bottom : 4pt">                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                                   <asp:TextBox ID="txtRecordOnCurrentPage" runat ="server"  Width="73px" CssClass="textboxgrey" Visible ="false"  ></asp:TextBox>      
                                                    </td>
                                                     
                                                </tr>  
                                         
            <tr>
                <td>
                    <iframe id="ifrmPrnt" runat="server" frameborder="0" src="BDRHistoryPrint.aspx" height="0"
                        width="0"></iframe>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
